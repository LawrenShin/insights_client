import React from 'react';
import {Grid, Typography} from "@material-ui/core";
import useStyles from "./useStyles";
import {paintRating} from "../../pages/results/prepareForGrid";

const AdvancedRatingWrapper = ({data, title}: any) => {
  const styles = useStyles();
  const {rating, strength} = data.essentialRating || data.advancedTotalRating;

  return (
    <Grid container sm={8} className={styles.paintContainer}>
      <span className={`${styles.titleFont}`}>{title}</span>
      <Grid container direction={'row'} className={styles.ratingStrength}>
        <Grid item>
          <Grid container direction={"column"}>
            <span className={styles.paleFont}>Rating:</span>
            <Grid container direction={'row'} alignItems={'center'}>
              <div className={paintRating(rating)}></div>
              <span>{rating}</span>
            </Grid>
          </Grid>
        </Grid>
        <Grid item className={styles.separator}> </Grid>
        <Grid item>
          <Grid container direction={"column"}>
            <span className={styles.paleFont}>Strength score:</span>
            <span>{strength}</span>
          </Grid>
        </Grid>
      </Grid>
    </Grid>)
}

export default AdvancedRatingWrapper;
