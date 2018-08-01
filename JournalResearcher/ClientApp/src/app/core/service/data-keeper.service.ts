import { Injectable } from '@angular/core';
import { SystemConstant } from '../../shared/constant';
import * as crypto from 'crypto-js';

@Injectable()
export class DataKeeperService {
  private dataHolder: any = {};

  constructor() { }

  private encryptData(unEncryptedData) {
    return crypto.AES.encrypt(unEncryptedData, SystemConstant.ENCRYPTIONKEY).toString();
  }

  private decryptData(encryptedData) {
    return crypto.AES.decrypt(encryptedData.toString(), SystemConstant.ENCRYPTIONKEY).toString(crypto.enc.Utf8);
  }

  public persistData(key, encryptedNumber) {
    const data = (key === '_sstt23tken5_' ? encryptedNumber : this.encryptData(encryptedNumber));
    sessionStorage.setItem(key, data);
  }

  public removePersitedData(key: string): void {
    window.sessionStorage.removeItem(key);
  }

  public removeAllPersistedData(): void {
    window.sessionStorage.clear();
  }

  public getPersistedData(key) {
    if (sessionStorage.getItem(key)) {
      return key === '_sstt23tken5_' ? sessionStorage.getItem(key) : this.decryptData(sessionStorage.getItem(key));
    } else {
      return false;
    }
  }

  private removePersistedData(key) {
    return sessionStorage.removeItem(key);
  }

  keepData(key, sharedData): void {
    if (key === 'role') {
      this.persistData('__jtoh67823_', sharedData);
    }

    if (key === 'token') {
      return this.persistData('_sstt23tken5_', sharedData);
    }

    if (key === 'key') {
      return this.persistData('_sj45jmker23h_', sharedData);
    }

    if (key === 'udata') {
      return this.persistData('_sjhdmer45dsr_', JSON.stringify(sharedData));
    }


    this.dataHolder[key] = sharedData;
  }

  public getData(key) {
    if (key === 'role') {
      return this.getPersistedData('__jtoh67823_') ?
        this.getPersistedData('__jtoh67823_') : false;
    }

    if (key === 'token') {
      return this.getPersistedData('_sstt23tken5_');
    }

    if (key === 'key') {
      return this.getPersistedData('_sj45jmker23h_');
    }
    if (key === 'udata') {
      return this.getPersistedData('_sjhdmer45dsr_');
    }

    return key ? this.dataHolder[key] : this.dataHolder;
  }

  removeData(key?) {
    if (key) {
      if (key === 'phoneNo') {
        this.removePersistedData('__ss67823');
      }
      delete this.dataHolder[key];
    } else {
      this.dataHolder = {};
    }
  }

}
