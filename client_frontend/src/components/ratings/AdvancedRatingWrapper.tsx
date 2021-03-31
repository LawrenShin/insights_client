import React from 'react';
import {Grid} from "@material-ui/core";
import useStyles from "./useStyles";
import {paintRating} from "../../pages/results/prepareForGrid";

const AdvancedRatingWrapper = ({data, title, children, sm, justify}: any) => {
  const styles = useStyles();
  const {rating, strength} = data.essentialRating || data.advancedTotalRating;

  return (
    <Grid item sm={sm} className={styles.paintContainer}>
      <span className={`${styles.titleFont}`}>{title}</span>
      <Grid justify={justify} container direction={'row'} className={styles.ratingStrength}>
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
      {children}
    </Grid>)
}

export default AdvancedRatingWrapper;
