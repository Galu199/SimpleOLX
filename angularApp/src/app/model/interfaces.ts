
export interface RefreshToken {
  id: number;
  userId: number;
  token: string;
  refreshCount: number;
  expiryDate: Date;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: RefreshToken;
  tokenType: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  firstname: string;
  lastname: string;
  password: string;
}

export interface RegisterResponse {
  status: number;
  message: string;
}

export interface Advert{
  title: string;
  description: string;
  mail: string;
  phoneNumber: string;
  price: number;
  localizationLatitude: string;
  localizationLongitude: number;
  category: string;
  image: string;
  userOwnerId: number;
}

export interface User {
  id: number;
  userName: string;
  normalizedUserName: string;
  email: string;
  normalizedEmail: string;
  emailConfirmed: boolean;
  passwordHash: string;
  securityStamp: string;
  concurrencyStamp: string;
  phoneNumber: string;
  twoFactorEnabled: boolean;
  lockoutEnd: string;
  lockoutEnabled: boolean;
  accessFailedCount: number;
  firstName: string;
  lastName: string;
  creationDate: string;
  advertsOwned: string;
}
