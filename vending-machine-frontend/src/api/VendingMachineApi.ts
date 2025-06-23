import axios from 'axios';
import { BrandDto, DrinkDto, PaymentDto, PaymentResultDto } from '../types';

const base = axios.create({
  baseURL: 'https://localhost:7105/api',
});

export const getBrands = async (): Promise<BrandDto[]> => {
  const res = await base.get('/brands');
  return res.data.data as BrandDto[];
};

export const getDrinks = async (): Promise<DrinkDto[]> => {
  const res = await base.get('/drinks');
  return res.data.data as DrinkDto[];
};

export const getFilteredDrinks = async (
  brandId?: number,
  minPrice?: number,
  maxPrice?: number
): Promise<DrinkDto[]> => {
  const res = await base.get('/drinks/filter', { params: { brandId, minPrice, maxPrice } });
  return res.data.data as DrinkDto[];
};

export const pay = async (dto: PaymentDto): Promise<PaymentResultDto> => {
  const res = await base.post<{
    flag: boolean;
    data: PaymentResultDto;
    message: string;
  }>('/payment/batch', dto);

  if (!res.data.flag) {
    throw new Error(res.data.message);
  }

  return res.data.data;
};

export const lockMachine = async (): Promise<boolean> => {
  const res = await base.post('/machine/lock');
  return res.data.flag;
};

export const unlockMachine = async (): Promise<void> => {
  await base.delete('/machine/lock');
};

export const getLockStatus = async (): Promise<boolean> => {
  const res = await base.get('/machine/lock/status');
  return res.data.data;
};
