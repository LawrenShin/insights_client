import React from 'react';
import {Grid} from "@material-ui/core";
import {useContryOptionsStyles} from "./useStyles";

const CountryOptions = (props: any) => {
  const styles = useContryOptionsStyles();

  return (
    <Grid container>
      <Grid item>
        <Grid container className={styles.searchCriteriasContainer}>
          <Grid item>
            <div className={styles.colTitle}>
              <span>Search Criteria</span>
            </div>
            <div className={styles.line}></div>
            <span className={styles.colSubTitle}>Region</span>
            <Grid container> </Grid>
          </Grid>
          <Grid item>
            <div className={styles.colTitle}>
              <span>Subregion/Country</span>
            </div>
            <div className={styles.line}></div>
            <span className={styles.colSubTitle}>By subregions category</span>
            <Grid container> </Grid>
          </Grid>
        </Grid>
      </Grid>
      <Grid item> </Grid>
    </Grid>
  );
}

export default CountryOptions;