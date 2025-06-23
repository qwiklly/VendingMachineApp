import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Catalog } from './pages/Catalog';
import { CartPage } from './pages/CartPage';
import { SelectionProvider } from './components/SelectionContext';
import { PaymentPage } from './pages/PaymentPage';
import { SuccessPage } from './pages/SuccessPage';
import { useEffect, useState } from 'react';
import { lockMachine, unlockMachine, getLockStatus } from './api/VendingMachineApi';

export const App = () => {
  const [locked, setLocked] = useState<boolean | null>(null);

  useEffect(() => {
    const attemptLock = async () => {
      const isLocked = await getLockStatus();
      if (isLocked) {
        setLocked(true);
      } else {
        const success = await lockMachine();
        setLocked(!success);
      }
    };

    attemptLock();

    const handleBeforeUnload = () => {
      unlockMachine();
    };

    window.addEventListener('beforeunload', handleBeforeUnload);

    return () => {
      window.removeEventListener('beforeunload', handleBeforeUnload);
      unlockMachine();
    };
  }, []);

  if (locked === null) return <p>Загрузка...</p>;
  if (locked) return <p className="errorText">Извините, в данный момент автомат занят.</p>;

  return (
    <BrowserRouter>
      <SelectionProvider>
        <Routes>
          <Route path="/" element={<Catalog />} />
          <Route path="/cart" element={<CartPage />} />
          <Route path="/payment" element={<PaymentPage />} />
          <Route path="/success" element={<SuccessPage />} />
        </Routes>
      </SelectionProvider>
    </BrowserRouter>
  );
};
