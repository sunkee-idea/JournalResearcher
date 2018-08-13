import { IConfirmSettings } from "./confirm-settings";

export interface IConfirmEmit {
  close?: boolean;
  message?: string;
  title?: string;
  resolve$?: any;
  override?: IConfirmSettings;
}
