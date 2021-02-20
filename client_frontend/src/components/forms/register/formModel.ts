const FormModel: FormModelType = {
  formId: 'registerForm',
  formField: {
    firstName: {
      name: 'firstName',
      label: 'First name*',
      requiredErrorMsg: 'First name is required'
    },
    lastName: {
      name: 'lastName',
      label: 'last name*',
      requiredErrorMsg: 'Last name is required'
    },
    email: {
      name: 'email',
      label: 'email*',
      requiredErrorMsg: 'Email is required',
    },
    phoneNumber: {
      name: 'phoneNumber',
      label: 'Phone (optional)'
    },
    companyName: {
      name: 'companyName',
      label: 'companyName*',
      requiredErrorMsg: 'Company name is required',
    },
    industry: {
      name: 'industry',
      label: 'industry*',
      requiredErrorMsg: 'Industry is required',
    },
    country: {
      name: 'country',
      label: 'country*',
      requiredErrorMsg: 'Country is required',
    },
    reasonOfInterest: {
      name: 'reasonOfInterest',
      label: 'reasonOfInterest',
      requiredErrorMsg: 'Reason of interest is a required field',
    },
    terms: {
      name: 'terms',
      label: 'I acknowledge that I agree to the <a href="#">Terms of Use</a> and have read <a href="#">Privacy Police.</a>'
    }
  }
}


interface FormModelItem {
  name: string;
  label: string;
  requiredErrorMsg?: string;
}

export type FormFieldType = {
  firstName: FormModelItem;
  lastName: FormModelItem;
  email: FormModelItem;
  phoneNumber: FormModelItem;
  companyName: FormModelItem;
  industry: FormModelItem;
  country: FormModelItem;
  reasonOfInterest: FormModelItem;
  terms: FormModelItem;
}

export interface FormModelType {
  formId: string;
  formField: FormFieldType;
}

export default FormModel;
