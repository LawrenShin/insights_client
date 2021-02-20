import React from 'react'
import Button from "../../button";
import useStyles from "./useStyles";
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import Input from '../../formFields/input';
import InputPassword from '../../formFields/inputPassword';


interface FormState {
  login: string;
  password: string;
}

// TODO: types
const SignIn = (props: any) => {
  const styles = useStyles();
  const initials = {
    login: '',
    password: '',
  }

  const _handleSubmit = (values: FormState) => {
    console.log(values);
  }

  return (<>
    <Formik
      initialErrors={initials}
      initialValues={initials}
      validationSchema={Yup.object().shape({
        login: Yup.string().required(),
        password: Yup.string().min(8, 'At least 8 characters long').required(),
      })}
      onSubmit={_handleSubmit}
    >
      {({values, setSubmitting}) => <Form id={'signIn'}
        className={`${styles.inputs} ${styles.fullWidth} ${styles.textAlignRight}`}>
        <Input
          name={'login'}
          label={'Login'}
          variant="outlined"
          size={'small'}
          value={values.login}
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

export default SignIn;
