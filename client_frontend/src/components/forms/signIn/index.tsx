import React from 'react'
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


interface FormState {
  username: string;
  password: string;
}

// TODO: types
const SignIn = (props: any) => {
  const styles = useStyles();
  const initials = {
    username: '',
    password: '',
  }

  const _handleSubmit = (values: FormState) => {
    props.loginUser(values);
  }

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
        <Button style={styles.button} type={'submit'}>Sign in</Button>
        {props.renderRegister(`${styles.button} ${styles.whiteBack}`)}
      </Form>}
    </Formik>
  </>)
}

export default connect(
  (state: RootState) => ({
    login: state.SignIn,
  }),
  (dispatch: Dispatch) => ({
    loginUser: (values: any) => dispatch(CreateAction(SignInType.SIGN_IN, values)),
  })
)(SignIn);
