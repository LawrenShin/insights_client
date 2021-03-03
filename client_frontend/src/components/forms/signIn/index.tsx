import React, {useEffect} from 'react'
import {connect} from 'react-redux';
import Button from "../../button";
import useStyles from "./useStyles";
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import Input from '../../formFields/input';
import InputPassword from '../../formFields/inputPassword';
import {RootState} from "../../../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../../store/actionType";
import {SignInType} from "../../../pages/SignIn/duck";
import {RequestStatuses} from "../../../api/requestTypes";
import CircularProgress from '@material-ui/core/CircularProgress';
import {useHistory} from "react-router-dom";


interface FormState {
  username: string;
  password: string;
}
interface UserData {
  token: string;
  accessRights: string[];
}

interface OwnProps {
  renderRegister: (styles: string) => JSX.Element;
}
interface StateProps {
  data: UserData;
  status: RequestStatuses;
  error: string | null;
}
interface DispatchProps {
  loginUser: (values: FormState) => void,
}
interface Props extends StateProps, DispatchProps, OwnProps {}


const SignIn = (props: Props) => {
  const {
    renderRegister,
    loginUser,
    data,
    status,
    error,
  } = props;
  const history = useHistory();
  const styles = useStyles();
  const initials = {
    username: '',
    password: '',
  }

  useEffect(() => {
    if (data) history.push('/searchCompanies');
  }, [data]);

  const _handleSubmit = (values: FormState) => {
    loginUser(values);
  }

  if (data) return null;
  return (<>
    <Formik
      initialErrors={initials}
      initialValues={initials}
      validationSchema={Yup.object().shape({
        username: Yup.string().required(),
        password: Yup.string().min(6, 'At least 6 characters long').required(),
      })}
      onSubmit={_handleSubmit}
    >
      {({values, setSubmitting}) => <Form id={'signIn'}
        className={`${styles.inputs} ${styles.fullWidth} ${styles.textAlignRight}`}>
        <Input
          name={'username'}
          label={'Username'}
          variant="outlined"
          size={'small'}
          value={values.username}
        />
        <InputPassword
          variant="outlined"
          size={'small'}
          value={values.password}
        />
        <a href={'#'}>forgot password?</a>
        {
          status === RequestStatuses.loading ?
          <CircularProgress /> : <Button style={styles.button} type={'submit'}>Sign in</Button>
        }
        {renderRegister(`${styles.button} ${styles.whiteBack}`)}
      </Form>}
    </Formik>
  </>)
}

export default connect(
  (state: RootState) => ({...state.SignIn}),
  (dispatch: Dispatch) => ({
    loginUser: (values: any) => dispatch(CreateAction(SignInType.SIGN_IN, values)),
  })
)(SignIn);
