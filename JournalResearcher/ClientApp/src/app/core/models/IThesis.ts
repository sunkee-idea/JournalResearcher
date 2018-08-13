export interface IThesis {
  title: string;
  author: string;
  reference: string;
  abstract: string;
  supervisorName: string;
  thesisDateTime: string;
  applicantId: string;
  thesisFile: string | any;
  isApproved?: boolean;
  action?:string;
  
}


export interface IPaginationModel {
  items: IThesis[],
  total: number
}
