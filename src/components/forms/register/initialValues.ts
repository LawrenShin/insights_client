import FormModel from "./formModel";

const {
  formField: {
    firstName,
    lastName,
    email,
    phoneNumber,
    industry,
    country,
    companyName,
    reasonOfInterest,
    terms,
  }
} = FormModel;

const initialValues = {
  [firstName.name]: '',
  [lastName.name]: '',
  [email.name]: '',
  [phoneNumber.name]: '',
  [industry.name]: '',
  [country.name]: '',
  [companyName.name]: '',
  [reasonOfInterest.name]: '',
  [terms.name]: false,
}

export default initialValues;
