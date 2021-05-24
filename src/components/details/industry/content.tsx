import React, {useEffect} from 'react';
import {Grid} from "@material-ui/core";
import useStyles from "./useStyles";
import Benchmark from "../../ratings/benchmark";
import RatingWrapper from "../../ratings/RatingWrapper";
import {keyTitle} from "../../../helpers";
import {renderSingleProp} from "../../../pages/details/helpers";


const renderRating = (ratingName: string, data: any) => <RatingWrapper
    title={keyTitle(ratingName)}
    sm={12}
    style={{ marginBottom: '5px' }}
  >
    <Grid container direction={'row'} style={{ marginTop: '20px' }}>
      {renderSingleProp('Rating', data.rating)}
    </Grid>
    <Benchmark data={data} />
  </RatingWrapper>

export const Content = ({data}: any) => {
  const styles = useStyles();

  if (!data) return null;

  return (
  <Grid container spacing={3} className={styles.root}>
    <Grid item xs={5} className={styles.paper}>
      <span>Essential ratings</span>
      {
        Object.keys(data.essentialRatings)
          .map(
            (ratingName) => data.essentialRatings[ratingName] ?
               renderRating(ratingName, data.essentialRatings[ratingName])
              :
              'N/A'
          )
      }
    </Grid>
    <Grid item xs={5} className={styles.paper}>
      <span>Advanced ratings</span>
    </Grid>
  </Grid>
  );
}