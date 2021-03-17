import React from 'react';
import {Grid, Typography} from "@material-ui/core";
import useStyles from "./useStyles";
import {Rounded} from "../../../button";

interface Props {
  toSignIn: () => void;
}

const Welcome = ({toSignIn}: Props) => {
  const styles = useStyles();


  return (<>
    <Grid container sm={12} spacing={5}>
      <Grid item sm={8}>
        <Typography className={`${styles.title} ${styles.welcome}`} variant={'h5'}>
          Welcome!
        </Typography>
        <Typography>
          We will send you an email.
        </Typography>
      </Grid>
      <Grid item sm={12} className={styles.buttonsContainer}>
        {/*TODO: give it request*/}
        <Rounded
          className={`${styles.button} ${styles.resend}`}
          type={'button'}>Resend contact email</Rounded>
        <span
          className={`${styles.button} ${styles.backToSignIn}`}
          onClick={toSignIn}
        >Back to Sign In</span>
      </Grid>
    </Grid>
  </>)
}

export default Welcome;
