import { createContext, useContext, useState } from 'react';
import { DrinkDto } from '../types';

interface SelectedDrink extends DrinkDto {
  count: number;
}

interface SelectionContextType {
  selected: SelectedDrink[];
  addDrink: (drink: DrinkDto) => void;
  updateCount: (id: number, count: number) => void;
  removeDrink: (id: number) => void;
  clear: () => void;
}

const SelectionContext = createContext<SelectionContextType | undefined>(undefined);

export const SelectionProvider = ({ children }: { children: React.ReactNode }) => {
  const [selected, setSelected] = useState<SelectedDrink[]>([]);

  const addDrink = (drink: DrinkDto) => {
    setSelected(prev => {
      const exists = prev.find(d => d.id === drink.id);
      if (exists) return prev;
      return [...prev, { ...drink, count: 1 }];
    });
  };

  const updateCount = (id: number, count: number) => {
    setSelected(prev => prev.map(d => d.id === id ? { ...d, count } : d));
  };

  const removeDrink = (id: number) => {
    setSelected(prev => prev.filter(d => d.id !== id));
  };

  const clear = () => setSelected([]);

  return (
    <SelectionContext.Provider value={{ selected, addDrink, updateCount, removeDrink, clear }}>
      {children}
    </SelectionContext.Provider>
  );
};

export const useSelection = () => {
  const ctx = useContext(SelectionContext);
  if (!ctx) throw new Error("useSelection must be used inside SelectionProvider");
  return ctx;
};
