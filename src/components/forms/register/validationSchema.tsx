import * as Yup from 'yup';
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

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/

const schema = [
  Yup.object().shape({
    [firstName.name]: Yup.string().required(firstName.requiredErrorMsg),
    [lastName.name]: Yup.string().required(lastName.requiredErrorMsg),
    [email.name]: Yup.string().email().required(email.requiredErrorMsg),
    [phoneNumber.name]: Yup.string().matches(phoneRegExp, 'Phone is not valid'),
  }),
  Yup.object().shape({
    [companyName.name]: Yup.string().required(companyName.requiredErrorMsg),
    [industry.name]: Yup.string().required(industry.requiredErrorMsg),
    [country.name]: Yup.string().required(country.requiredErrorMsg),
  }),
  Yup.object().shape({
    [reasonOfInterest.name]: Yup.string().required(reasonOfInterest.requiredErrorMsg),
    [terms.name]: Yup.boolean().required(terms.requiredErrorMsg),
  })
]

export default schema;
