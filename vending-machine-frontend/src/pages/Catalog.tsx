import { useEffect, useState } from 'react';
import { getBrands, getFilteredDrinks } from '../api/VendingMachineApi';
import { BrandDto, DrinkDto } from '../types';
import { useSelection } from '../components/SelectionContext';
import './Catalog.css';

import { useNavigate } from 'react-router-dom';

export const Catalog = () => {
  const [brands, setBrands] = useState<BrandDto[]>([]);
  const [drinks, setDrinks] = useState<DrinkDto[]>([]);
  const [selectedBrand, setSelectedBrand] = useState<number | null>(null);
  const [priceRange, setPriceRange] = useState<[number, number]>([0, 110]);

  const { addDrink, removeDrink, selected } = useSelection();

  const navigate = useNavigate();

  const toggleDrink = (drink: DrinkDto) => {
    const isSelected = selected.some(d => d.id === drink.id);
    if (isSelected) {
      removeDrink(drink.id);
    } else {
      addDrink(drink);
    }
  };

  useEffect(() => {
    (async () => {
      const allBrands = await getBrands();
      setBrands(allBrands);
      const filtered = await getFilteredDrinks();
      setDrinks(filtered);
    })();
  }, []);

  useEffect(() => {
    const fetch = async () => {
      const filtered = await getFilteredDrinks(
        selectedBrand ?? undefined,
        priceRange[0],
        priceRange[1]
      );
      setDrinks(filtered);
    };
    fetch();
  }, [selectedBrand, priceRange]);

  return (
    <div className="container">
      <div className="header">
        <h1>Газированные напитки</h1>
        <button className="importButton">Импорт</button>
      </div>

      <div className="filters">
        <div className="filterGroup">
          <label htmlFor="brandSelect" className="filterLabel">Выберите бренд</label>
          <select
            id="brandSelect"
            className="select"
            value={selectedBrand ?? ''}
            onChange={e => setSelectedBrand(e.target.value === '' ? null : +e.target.value)}
          >
            <option value="">Все бренды</option>
            {brands.map(b => (
              <option key={b.id} value={b.id}>{b.name}</option>
            ))}
          </select>
        </div>

        <div className="filterGroup priceRangeWrapper">
          <label htmlFor="priceRange" className="filterLabel">Стоимость</label>
          <input
            id="priceRange"
            className="rangeInput"
            type="range"
            min="0"
            max="110"
            value={priceRange[1]}
            onChange={e => setPriceRange([0, +e.target.value])}
          />
          <div>до {priceRange[1]} руб.</div>
        </div>

        <button
          className={`button ${selected.length ? 'buttonEnabled' : 'buttonDisabled'}`}
          disabled={!selected.length}
          onClick={() => navigate('/cart')}
        >
          Выбрано: {selected.length}
        </button>
      </div>

      <div className="catalogGrid">
        {drinks.map(drink => {
          const isSelected = selected.some(d => d.id === drink.id);
          return (
            <div key={drink.id} className="catalogItem">

              <img src={`/images/${drink.brandName}.png`} alt={drink.brandName} className="itemImage" />

              <div className="itemName">{drink.name}</div>
              <div className="itemPrice">{drink.price} руб.</div>
              <button
                className={`selectButton ${drink.quantity === 0
                    ? 'selectButtonDisabled'
                    : isSelected
                      ? 'selectedButton'
                      : 'selectButtonEnabled'
                  }`}
                disabled={drink.quantity === 0}
                onClick={() => toggleDrink(drink)}
              >
                {drink.quantity === 0 ? 'Закончился' : isSelected ? 'Выбрано' : 'Выбрать'}
              </button>
            </div>
          );
        })}
      </div>
    </div>
  );
};
