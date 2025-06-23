export interface BrandDto {
  id: number;
  name: string;
}

export interface DrinkDto {
  id: number;
  name: string;
  price: number;
  quantity: number;
  brandName: string;
}

export interface PaymentDto {
  items: { drinkId: number; count: number }[];
  coins1: number;
  coins2: number;
  coins5: number;
  coins10: number;
}

export interface PaymentResultDto {
  message: string;
  change: { [coin: number]: number };
}

export interface PaymentResultDto {
  message: string;
  change: { [coin: number]: number };
}