import React, {useState} from 'react';
import {Form, Formik} from 'formik';
import initialValues from "./initialValues";
import validationSchema from "./validationSchema";
import FormModel from './formModel';
import PersonalInfo from "./steps/personalInfo";
import CompanyInfo from "./steps/companyInfo";
import DiSvg from "../../DiSvg";
import {Squared} from '../../button';
import useStyles from "./useStyles";
import CreateAccount from "./steps/createAccount";
import {connect} from "react-redux";
import {RootState} from "../../../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../../store/actionType";
import {RegisterTypes} from "./duck";
import {RequestStatuses} from "../../../api/requestTypes";
import CircularProgress from '@material-ui/core/CircularProgress';
import {usePrevious} from "../../../helpers";
import Welcome from "./steps/welcome";


const steps = ['personalInfo', 'companyInfo', 'createAccount'];
const {formId, formField} = FormModel;


interface StateProps {
  status: RequestStatuses;
  error: null;
}
interface DispatchProps {
  regUser: (values: any) => void;
}

interface Props extends StateProps, DispatchProps {
  toSignIn: () => void;
}

const renderStepContent = (step: number) => {
  if (step === 0) return <PersonalInfo formField={formField} />
  if (step === 1) return <CompanyInfo formField={formField} />
  if (step === 2) return <CreateAccount formField={formField} />

  // TODO: if step -1 push to history go back to login. It will be two independent pages
  return 'dough';
}
// COMPONENT
const Register = ({
    toSignIn,
    regUser,
    status,
    // TODO: add error response
    error,
  }: Props) => {
  const [step, setStep] = useState<number>(0);
  const currentValidationSchema = validationSchema[step];
  const isLastStep = step === steps.length - 1;
  const styles = useStyles();
  const prevStatus = usePrevious(status);

  const submitForm = (values: any, actions: any) => {
    regUser(values);
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

  const renderButtons = () => <div className={styles.buttonContainer}>
    <span
      className={styles.backButton}
      onClick={() => step === 0 ? toSignIn() : handleBack()}
    >← Back</span>
    <Squared
      type="submit"
      className={styles.nextButton}
    >{step === 2 ? 'Create account' : 'Next step'}</Squared>
  </div>


  return (<>
    <div className={styles.DiSvgContainer}>
      <DiSvg />
    </div>
    { (step === 2 && prevStatus === RequestStatuses.loading && status === RequestStatuses.still) ?
      <Welcome toSignIn={toSignIn} />
        :
      (<Formik
        onSubmit={handleSubmit}
        initialValues={initialValues}
        validationSchema={currentValidationSchema}
      >
        {({values}) => (
          <Form id={formId} style={{width: '100%'}}>
            {renderStepContent(step)}
            {status !== RequestStatuses.loading ?
              renderButtons() : <div className={styles.loaderContainer}><CircularProgress /></div>}
          </Form>
        )}
      </Formik>)
    }
  </>);
}

export default connect(
  ({Register: {status, error}}: RootState): StateProps => ({
    status,
    error
  }),
  (dispatch: Dispatch): DispatchProps => ({
    regUser: (values: any) => dispatch(CreateAction(RegisterTypes.REGISTER, values)),
  })
)(Register);
