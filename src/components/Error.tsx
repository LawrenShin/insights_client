import React from 'react';
import {useHistory} from "react-router-dom";
import {Squared} from "./button";
import {makeStyles} from "@material-ui/core";

const useStyles = makeStyles({
  root: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    '& p h1': {width: 'fit-content'},
  }
});

const Error = ({ error }: any) => {
  const history = useHistory();
  const styles = useStyles();

  return (<>
    <div className={styles.root}>
      <h1>An error occurred.</h1>
      <p>Message: {error?.message}</p>
      <Squared
        style={{ width: 'fit-content' }}
        onClick={() => history.push('/mainSearch')}
      >
        Back to main search
      </Squared>
    </div>
  </>);
}

export default Error;
