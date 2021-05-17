import React from 'react';
import {Grid, Typography} from "@material-ui/core";
import useStyles from "./useStyles";
import {Rounded} from "../../../button";
import {Search} from "@material-ui/icons";

interface Props {
  toSignIn?: () => void;
  // will render inner welcome
  classes?: any;
}

const Welcome = ({toSignIn, classes}: Props) => {
  const styles = useStyles();
  // NOTE: 2 types of greetings. with classes for the mainSearch page the other for signup

  if (classes) return(
    <div style={{ width: '50%' }}>
      <div className={classes.titlesContainer}>
        <Typography variant={'h4'}>Welcome to DI insights!</Typography>
        <span>Are you ready to explore?</span>
      </div>
      <div className={classes.searchExplanation}>
        <div className={classes.iconContainer}>
          <Search />
        </div>
        <div>
          <Typography variant={'h6'}>Search bar</Typography>
          <span>Just use the search bar if you want to find a specific company, industry or country. Enter a keyword and get the result in seconds.</span>
        </div>
      </div>
    </div>
  );

  return (<>
    <Grid container sm={12} spacing={5}>
      <>
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
      </>
    </Grid>
  </>)
}

export default Welcome;
