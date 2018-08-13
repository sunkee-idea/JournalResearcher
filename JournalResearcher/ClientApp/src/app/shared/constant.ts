import {environment} from "../../environments/environment";
const BASEURL = environment.BASE_URL;

export const routes ={
  LOGIN:BASEURL + "token",
  LOGGEDIN: BASEURL + "auth/logged",
  GETACCOUNT: BASEURL + "auth/getAccount",
  REGISTER: BASEURL + "auth/signup",

  SUBMITTHESIS: BASEURL +"journal/create",
  GETTHESIS: BASEURL + "journal/get/",

  GETALLTHESIS: BASEURL + "journal/getThesis",
  APPROVE:BASEURL + "journal/approveThesis"
  


};

export const SystemConstant ={
  ENCRYPTIONKEY:"</@$!!Sliver#hkournal&!!)/>",
  SESSIONKEY:"_hhaomfdsdssrhy234_",
  LEVEL_ACCESS:{
    'u':'User',
    'a':'Admin',
  }

};
