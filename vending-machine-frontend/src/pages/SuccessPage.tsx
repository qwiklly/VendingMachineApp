import { useLocation, useNavigate } from 'react-router-dom';
import { PaymentResultDto } from '../types';
import './SuccessPage.css';

export const SuccessPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const state = location.state as PaymentResultDto | undefined;

  const change = state?.change ?? {};
  const totalChange = Object.entries(change).reduce(
    (sum, [coin, count]) => sum + Number(coin) * count, 0
  );

  const coinOrder = [1, 2, 5, 10];

  const formatLine = (coin: number) => {
    const count = change[coin] || 0;
    return `${count} монет по ${coin}₽`;
  };

  return (
    <div className="successContainer">
      <h2>Спасибо за покупку!</h2>
      {totalChange > 0 ? (
        <>
          <p>Пожалуйста, возьмите вашу сдачу:</p>
          <h3 className="greenText">{totalChange} руб.</h3>
          <h4>Ваши монеты:</h4>
          <div className="coinsList">
            {coinOrder.map(c => (
              <p key={c}>{formatLine(c)}</p>
            ))}
          </div>
        </>
      ) : (
        <p className="errorText">
          Извините, в данный момент мы не можем продать вам товар по причине того, что автомат не может выдать вам нужную сдачу.
        </p>
      )}
      <button className="catalogButton" onClick={() => navigate('/')}>
        Каталог напитков
      </button>
    </div>
  );
};
