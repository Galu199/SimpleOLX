
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
