import { useNavigate } from 'react-router-dom';
import { useSelection } from '../components/SelectionContext';
import { PaymentDto } from '../types';
import { pay } from '../api/VendingMachineApi';
import { useState } from 'react';
import './PaymentPage.css';

const coinValues = [1, 2, 5, 10];

export const PaymentPage = () => {
  const navigate = useNavigate();
  const { selected, clear } = useSelection();
  const [coins, setCoins] = useState<Record<number, number>>({
    1: 0, 2: 0, 5: 0, 10: 0
  });

  const totalPrice = selected.reduce((sum, d) => sum + d.price * d.count, 0);
  const totalInserted = Object.entries(coins)
    .reduce((sum, [val, cnt]) => sum + Number(val) * cnt, 0);

  const changeCoin = (value: number, delta: number) =>
    setCoins(prev => ({ ...prev, [value]: Math.max(0, prev[value] + delta) }));

  const onInput = (value: number, ev: React.ChangeEvent<HTMLInputElement>) => {
    const num = Math.max(0, Number(ev.target.value) || 0);
    setCoins(prev => ({ ...prev, [value]: num }));
  };

  const handlePay = async () => {
    const dto: PaymentDto = {
      items: selected.map(d => ({ drinkId: d.id, count: d.count })),
      coins1: coins[1],
      coins2: coins[2],
      coins5: coins[5],
      coins10: coins[10],
    };

    try {
      const result = await pay(dto);

      if (!result) {
        throw new Error('Сервер не вернул данные об оплате');
      }

      clear();
      navigate('/success', { state: result });
    } catch (err: unknown) {
      if (err instanceof Error) {
        alert(err.message);
      } else {
        alert('Произошла неизвестная ошибка');
      }
    }
  };

  return (
    <div className="container">
      <h1 className="redText">Оплата</h1>
      <table className="paymentTable">
        <thead>
          <tr>
            <th>Номинал</th>
            <th>Количество</th>
            <th>Сумма</th>
          </tr>
        </thead>
        <tbody>
          {coinValues.map(v => (
            <tr key={v}>
              <td>
                <div className="coinCircle">{v}</div> {v} руб.
              </td>
              <td>
                <div className="countControl">
                  <button onClick={() => changeCoin(v, -1)}>-</button>
                  <input
                    type="number"
                    min={0}
                    value={coins[v]}
                    onChange={e => onInput(v, e)}
                  />
                  <button onClick={() => changeCoin(v, +1)}>+</button>
                </div>
              </td>
              <td><strong>{v * coins[v]} руб.</strong></td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="paymentFooter">
        <button className="backButton" onClick={() => navigate('/cart')}>
          Вернуться
        </button>

        <div className="paymentRight">
          <div className="paymentStatusRow">
            <div className="totalPrice">
              Итоговая сумма: <strong>{totalPrice} руб.</strong>
            </div>
            <div className="paymentStatus">
              Вы внесли:{' '}
              <span className={totalInserted < totalPrice ? 'inserted notEnough' : 'inserted enough'}>
                <strong>{totalInserted} руб.</strong>
              </span>
            </div>
          </div>

          <button
            className="payButton"
            disabled={totalInserted < totalPrice}
            onClick={handlePay}
          >
            Оплатить
          </button>
        </div>
      </div>
    </div>
  );
};
