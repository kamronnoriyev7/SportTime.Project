export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  fullName: string;
  createdAt: Date;
  lastLoginAt?: Date;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
}

export interface AuthResponse {
  token: string;
  expiration: Date;
  user: User;
}