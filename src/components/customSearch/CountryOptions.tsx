import React from 'react';
import {Grid} from "@material-ui/core";
import {useContryOptionsStyles} from "./useStyles";
import {renderOptions} from "../../pages/details/helpers";
import ArrowForwardIosIcon from '@material-ui/icons/ArrowForwardIos';

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
            <Grid container className={styles.regionsContainer}>
              {/* MOCK */}
              {renderOptions([
                {id: Math.random(), name: 'Africa'},
                {id: Math.random(), name: 'Asia'},
                {id: Math.random(), name: 'Europe'},
                {id: Math.random(), name: 'Oceania'},
                {id: Math.random(), name: 'Middle East'},
                {id: Math.random(), name: 'The Americas'},
              ], () => <div className={styles.arrowContainer}>
                <ArrowForwardIosIcon style={{ fontSize: '.8em'}} />
              </div>)}
            </Grid>
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