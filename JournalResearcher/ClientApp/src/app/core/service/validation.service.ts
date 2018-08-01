
import { AbstractControl, FormGroup } from '@angular/forms';

export class ValidationService {


  static getValidatorErrorMessage(code: string) {
    const config = {
      'required': 'Required',
      'invalidEmailAddress': 'Invalid email address',
      'invalidPassword': 'Invalid password. Password must be at least 6 characters long, and contain a number.',
      'invalidMatch': 'Password do not match'
    };
    return config[code];
  }

  static emailValidator(control?: AbstractControl) {
    //RFC 2822 compliant Regex
    if (control.value.match(/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/)) {
      return null;
    } else {
      return { 'invalidEmailAddress': true }
    }
  }

  static passwordValidator(control?: AbstractControl) {
    // {6,100}           - Assert password is between 6 and 100 characters
    // (?=.*[0-9])       - Assert a string has at least one number
    // (?!.*\s)          - Spaces are not allowed
    if (control.value.match(/^(?=.*\d)(?=.*[a-zA-Z!@#$%^&*])(?!.*\s).{6,100}$/)) {
      return null;
    } else {
      return { 'invalidPassword': true };
    }
  }

  static validateMatch(g: FormGroup) {
    let password = g.get('password').value
    let confirmpassword = g.get('confirmpassword').value
    // let password = registrationFormGroup.controls.password.value;
    // let repeatPassword = registrationFormGroup.controls.confirmpassword.value;

    if (confirmpassword.length <= 0) {
      return null;
    }

    if (confirmpassword !== password) {
      return {
        doesMatchPassword: true
      };
    }
    return null;

  }



  static passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmpassword').value
      ? null : { 'mismatch': true };
  }

  static generatePassword(length) {
    var chars = "abc3899d453e()-+<>43fghi6787990jk900l08576mnuvwxyzABCDEF%^&*GHIJKLMNOP1234567890!@#$(";
    let pass = "";
    for (var x = 0; x < length; x++) {
      var i = Math.floor(Math.random() * chars.length);
      pass += chars.charAt(i);
      if (chars.charAt(i).toUpperCase().length == 0) {
        pass += chars.charAt(i).toUpperCase();
      }
    }

    return pass;
  }

  


}

