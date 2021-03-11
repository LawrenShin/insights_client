import React from 'react';
import Header from "../../components/Header";
import useStyles from "./useStyles";
import {Typography} from "@material-ui/core";


const Results = () => {
  const styles = useStyles();

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>

        <span>Main search</span>
        <Typography variant={'h5'}>
          List of companies
        </Typography>

        <div className={styles.content}>

        </div>
      </div>
    </div>
  )
}

export default Results;
