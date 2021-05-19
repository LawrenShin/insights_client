import React from 'react';
import {Grid} from "@material-ui/core";
import useStyles from "./useStyles";
import {paintRatingClass} from "../../pages/results/prepareForGrid";
// TODO: can rafactor in unicontainer
export interface Props {
  data?: {
    rating: string;
    strength: number;
    score: number;
  },
  justify?: "center" | "flex-start" | "flex-end" | "space-between" | "space-around" | "space-evenly";
  children?: any;
}

const AdvancedRatingWrapper = ({data, children, justify}: Props) => {
  const styles = useStyles();

  return (
    <>{(data?.rating && data?.strength) && <Grid
        container
        justify={justify}
        direction={'row'}
        className={styles.ratingStrength}
      >
        <Grid item>
          <Grid container direction={"column"}>
            <span className={styles.paleFont}>Rating:</span>
            <Grid container direction={'row'} alignItems={'center'}>
              <div className={paintRatingClass(data.rating)}></div>
              <span>{data.rating}</span>
            </Grid>
          </Grid>
        </Grid>
        <Grid item className={styles.separator}> </Grid>
        <Grid item>
          <Grid container direction={"column"}>
            <span className={styles.paleFont}>Strength score:</span>
            <span>{data.strength}</span>
          </Grid>
        </Grid>
      </Grid>}
      {children}
    </>
  )
}

export default AdvancedRatingWrapper;
