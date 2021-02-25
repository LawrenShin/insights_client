import React, {useState} from 'react';
import { Formik, Form } from 'formik';
import initialValues from "./initialValues";
import validationSchema from "./validationSchema";
import FormModel from './formModel';
import PersonalInfo from "./steps/personalInfo";
import CompanyInfo from "./steps/companyInfo";
import DiSvg from "../../DiSvg";
import Button from '../../button';
import useStyles from "./useStyles";
import CreateAccount from "./steps/createAccount";

const steps = ['personalInfo', 'companyInfo', 'createAccount'];
const {formId, formField} = FormModel;

const renderStepContent = (step: number) => {
  if (step === 0) return <PersonalInfo formField={formField} />
  if (step === 1) return <CompanyInfo formField={formField} />
  if (step === 2) return <CreateAccount formField={formField} />

  // TODO: if step -1 push to history go back to login. It will be two independent pages
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
    { step === 3 ?
      ('finale')
        :
      (<Formik
        onSubmit={handleSubmit}
        initialValues={initialValues}
        validationSchema={currentValidationSchema}
      >
        {({values}) => (
          <Form id={formId} style={{width: '100%'}}>
            {renderStepContent(step)}
            <div className={styles.buttonContainer}>
              <span
                className={styles.backButton}
                onClick={() => setStep(step - 1)}
              >← Back</span>
              <Button
                type="submit"
                className={styles.nextButton}
              >{step === 2 ? 'Create account' : 'Next step'}</Button>
            </div>
          </Form>
        )}
      </Formik>)
    }
  </>);
}

export default Register;
