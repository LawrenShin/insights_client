import React from 'react';
import RatingWrapper, {WrapperModes} from "../../components/ratings/RatingWrapper";
import {Grid} from "@material-ui/core";
import PieRating from "../../components/ratings/PieRating";
import useStyles from "./useStyles";


export enum Races {
  asian = 'asian',
  caucasian = 'caucasian',
  black = 'black',
  arab = 'arab',
}
export type Race = 'asian' | 'caucasian' | 'black' | 'arab';
export type Gender = 'female' | 'male';

interface Props {
  title: string,
  data: {
    genderStats: {
      [key in Gender]: { name: Gender, percentage: number }
    },
    raceStats: {
      [key in Race]: { name: Race, percentage: number }
    },
  }
}

const PieCharts = ({ data, title }: Props) => {
  const styles = useStyles();


  return (
    <RatingWrapper
      mode={WrapperModes.advanced}
      title={title}
      sm={4}
    >
      <Grid container direction={'row'} spacing={3}>
        <Grid item sm={6} className={styles.pieChartContainer}>
          <span>Gender Ratio</span>
          <PieRating
            data={data.genderStats}
            height={150}
          />
        </Grid>
        <Grid item sm={6} className={styles.pieChartContainer}>
          <span>Race/Ethnicity Ratio</span>
          <PieRating data={data.raceStats} height={150} />
        </Grid>
      </Grid>
    </RatingWrapper>
  );
}

export default PieCharts;