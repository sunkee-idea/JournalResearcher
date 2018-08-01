export interface IUser {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  password: string;
  confirmPassword: string;
  email: string;
  role:string;
}

export interface IUserLogin {
  password: string;
  email: string;
}

export interface ITokenparams {
  AccessToken: string;
  token_type: string;
  expires_in: string;
  Key: string;
  Role: string;
}
