import React from 'react';
import DiSvg from "../../components/DiSvg";
import useStyles from "./useStyles";
import SignIn from "../../components/forms/signIn";


const SignInPage = (props: any) => {
  const styles = useStyles();

  return (<>
        <DiSvg styles={styles.DiSvgContainer} />
        <SignIn {...props} />
      </>);
}

export default SignInPage;
