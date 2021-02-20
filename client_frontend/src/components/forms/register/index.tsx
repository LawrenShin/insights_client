import React, {useState} from 'react';
import { Formik, Form } from 'formik';
import initialValues from "./initialValues";
import validationSchema from "./validationSchema";
import FormModel from './formModel';
import PersonalInfo from "./steps/personalInfo";
import CompanyInfo from "./steps/companyInfo";
import DiSvg from "../../DiSvg";
import {Grid} from "@material-ui/core";
import {inspect} from "util";
import useStyles from "./useStyles";

const steps = ['personalInfo', 'companyInfo', 'createAccount', 'welcome'];
const {formId, formField} = FormModel;

const renderStepContent = (step: number) => {
  if (step === 0) return <PersonalInfo formField={formField} />
  // if (step === 2) return <CompanyInfo formField={formField} />

  return 'dough';
}

const Register = () => {
  const [step, setStep] = useState<number>(0);
  const currentValidationSchema = validationSchema[step];
  const isLastStep = step === steps.length - 1;
  const styles = useStyles();

  const submitForm = (values: any, actions: any) => {
    console.log(values, 'SUBMIT');
    actions.setSubmitting(false);
  }

  const handleSubmit = (values: any, actions: any) => {
    if (isLastStep) {
      submitForm(values, actions);
    } else {
      setStep(step + 1);
      actions.setTouched({});
      actions.setSubmitting(false);
    }
  }

  const handleBack = () => {
    setStep(step - 1);
  }

  return (<>
    <div className={styles.DiSvgContainer}>
      <DiSvg />
    </div>
    { step === steps.length ?
      ('finale')
        :
      (<Formik
        onSubmit={handleSubmit}
        initialValues={initialValues}
        validationSchema={currentValidationSchema}
      >
        {({values}) => (<Form id={formId}>
          {renderStepContent(step)}
        </Form>)}
      </Formik>)
    }
  </>);
}

export default Register;
