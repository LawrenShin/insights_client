import React, {useEffect} from 'react';
import {Grid} from "@material-ui/core";
import useStyles from "./useStyles";
import Benchmark from "../../ratings/benchmark";
import RatingWrapper, {WrapperModes} from "../../ratings/RatingWrapper";
import {keyTitle} from "../../../helpers";
import {renderSingleProp} from "../../../pages/details/helpers";
import List from "../../charts/list";


const renderRating = (ratingName: string, data: any) => <RatingWrapper
    key={`${Math.random()}${Math.random()}`}
    title={keyTitle(ratingName)}
    sm={12}
    style={{ marginBottom: '5px' }}
  >
    <Grid container direction={'row'} style={{ marginTop: '20px', fontSize: '.8em'  }}>
      {renderSingleProp('Average score', data.score)}
    </Grid>
    <Benchmark data={data} />
  </RatingWrapper>

export const Content = ({data}: any) => {
  const styles = useStyles();

  if (!data) return null;

  return (
  <Grid container className={styles.root}>
    <Grid item xs={5} className={styles.paper}>
      <div className={styles.title}>
        <span>Essential ratings</span>
      </div>
      {
        Object.keys(data.essentialRatings)
          .map(
            (ratingName) => (data.essentialRatings[ratingName] && data.essentialRatings[ratingName]?.score) ?
               renderRating(ratingName, data.essentialRatings[ratingName])
              :
              null
          )
      }
      {data?.essentialRatings?.essentialRatingBars && <RatingWrapper
        mode={WrapperModes.advanced}
        title={keyTitle('essentialRatingBars')}
        data={data.essentialRatingBars}
        justify={'space-between'}
        sm={12}
      >
        <List data={data.essentialRatings.essentialRatingBars} />
      </RatingWrapper>}
    </Grid>
    <Grid item xs={5} className={styles.paper}>
      <div className={styles.title}>
        <span>Advanced ratings</span>
      </div>
      {
        Object.keys(data.advancedRatings)
          .map(
            (ratingName) => (data.advancedRatings[ratingName] && data.advancedRatings[ratingName]?.score) ?
              renderRating(ratingName, data.advancedRatings[ratingName])
              :
              null
          )
      }
      {data?.advancedRatings?.advancedRatingBars && <RatingWrapper
        mode={WrapperModes.advanced}
        title={keyTitle('advancedRatingBars')}
        data={data.advancedRatingBars}
        justify={'space-between'}
        sm={12}
      >
        <List
          // classes={styles.littleFont}
          data={data.advancedRatings.advancedRatingBars}
        />
      </RatingWrapper>}
    </Grid>
  </Grid>
  );
}