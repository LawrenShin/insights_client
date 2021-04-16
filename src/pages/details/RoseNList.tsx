import React from 'react';
import RatingWrapper, {WrapperModes} from "../../components/ratings/RatingWrapper";
import Radar from "../../components/charts/radarChart";
import {paintRating} from "../../components/charts/useStyles";
import List from "../../components/charts/list";
import {Grid} from "@material-ui/core";
import useStyles from "./useStyles";

const RoseNList = ({ data, isAdvanced }: any) => {
  const styles = useStyles();

  return (
    <Grid direction={'row'} container style={{gap: '5px'}} wrap={'nowrap'}>
      <RatingWrapper
        mode={WrapperModes.advanced}
        title={`${isAdvanced ? 'Advanced' : 'Essential'} Rating`}
        data={data.ratingsWindRose}
        sm={8}
      >
        <Radar
          paintRating={paintRating}
          data={data.ratingsWindRose}
        />
      </RatingWrapper>
      {(data.ratingBars || data.ratingsWindRose) && <RatingWrapper
        mode={WrapperModes.advanced}
        title={`${isAdvanced ? 'Advanced' : 'Essential'} Sub Scores`}
        data={isAdvanced ? data.ratingsWindRose : data.ratingBars}
        justify={'space-between'}
        sm={4}
      >
        <List
          classes={styles.littleFont}
          data={isAdvanced ? data.ratingsWindRose : data.ratingBars}
        />
      </RatingWrapper>}
    </Grid>
  );
}

export default RoseNList;