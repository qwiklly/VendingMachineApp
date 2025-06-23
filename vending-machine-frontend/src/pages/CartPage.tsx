import { useSelection } from '../components/SelectionContext';
import './CartPage.css';
import { useNavigate } from 'react-router-dom';

export const CartPage = () => {
  const { selected, updateCount, removeDrink } = useSelection();
  const navigate = useNavigate();

  const total = selected.reduce((sum, item) => sum + item.price * item.count, 0);

  const handleCountChange = (id: number, newCount: number, max: number) => {
    if (newCount >= 1 && newCount <= max) {
      updateCount(id, newCount);
    }
  };

  if (selected.length === 0) {
    return (
      <div className="container">
        <h1 className="redText">У вас нет ни одного товара</h1>
        <button className="backButton" onClick={() => navigate('/')}>Вернуться на страницу каталога</button>
      </div>
    );
  }

  return (
    <div className="container">
      <h1 className="redText">Оформление заказа</h1>
      <table className="cartTable">
        <thead>
          <tr>
            <th>Товар</th>
            <th>Количество</th>
            <th>Цена</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {selected.map(item => (
            <tr key={item.id}>
              <td className="itemInfo">
                <img src={`/images/${item.brandName}.png`} alt={item.brandName} className="cartImage" />
                <div>{item.name}</div>
              </td>
              <td>
                <div className="countControl">
                  <button onClick={() => handleCountChange(item.id, item.count - 1, item.quantity)}>-</button>
                  <input
                    type="number"
                    min={1}
                    max={item.quantity}
                    value={item.count}
                    onChange={e => handleCountChange(item.id, +e.target.value, item.quantity)}
                  />
                  <button onClick={() => handleCountChange(item.id, item.count + 1, item.quantity)}>+</button>
                </div>
              </td>
              <td><strong>{item.price * item.count} руб.</strong></td>
              <td>
                <button className="deleteBtn" onClick={() => removeDrink(item.id)}>🗑</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="cartFooter">
        <button className="backButton" onClick={() => navigate('/')}>Вернуться</button>
        <div className="rightBlock">
          <div className="total">
            Итоговая сумма: <strong>{total} руб.</strong>
          </div>
          <button className="payButton" onClick={() => navigate('/payment')}>Оплата</button>
        </div>
      </div>
    </div>
  );
};
