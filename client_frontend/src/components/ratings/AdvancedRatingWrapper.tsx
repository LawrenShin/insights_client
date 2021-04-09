import React from 'react';
import {Grid} from "@material-ui/core";
import useStyles from "./useStyles";
import {paintRatingClass} from "../../pages/results/prepareForGrid";
// TODO: can rafactor in unicontainer
interface Props {
  data?: {
    rating: string;
    strength: number;
    score: number;
  },
  title?: string;
  classes?: string;
  sm?:  boolean | "auto" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12;
  justify?: "center" | "flex-start" | "flex-end" | "space-between" | "space-around" | "space-evenly";
  style?: any;
  children?: any;
}

const AdvancedRatingWrapper = ({data, title, children, sm, justify, style, classes}: Props) => {
  const styles = useStyles();

  return (
    <Grid item sm={sm} className={`${styles.paintContainer} ${classes}`} style={style}>
      <span className={`${styles.titleFont}`}>{title}</span>
      {(data?.rating && data?.strength) && <Grid justify={justify} container direction={'row'} className={styles.ratingStrength}>
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
    </Grid>)
}

export default AdvancedRatingWrapper;
