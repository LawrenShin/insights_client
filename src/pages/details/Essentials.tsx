import React from 'react';
import {Grid} from "@material-ui/core";
import EssentialRadialRating from "../../components/ratings/EssentialRating";
import {renderSingleProp} from "./helpers";
import useStyles from "./useStyles";

const Essentials = ({ data, isAdvanced }: any) => {
  const styles = useStyles();

  return (<>
    {data && <Grid container style={{gap: '5px'}} wrap={'nowrap'}>
      {data.essentialRating && <Grid
        sm={4}
        item
        className={`${styles.paintContainer} ${styles.essentialContainerSpacing}`}
      >
        <EssentialRadialRating
          title={'Essential Rating'}
          styles={styles}
          data={data.essentialRating}
          renderSingleProp={renderSingleProp}
        />
      </Grid>}
      {(data.advancedTotalRating || data.essentialRatingDiversityScore) &&
        <Grid sm={4} item className={`${styles.paintContainer} ${styles.essentialContainerSpacing}`}>
          <EssentialRadialRating
            title={isAdvanced ? 'Advanced Total Rating' : "Essential Diversity Rating"}
            styles={styles}
            data={data[isAdvanced ? 'advancedTotalRating' : 'essentialRatingDiversityScore']}
            renderSingleProp={renderSingleProp}
          />
        </Grid>
      }
      {(data.essentialRatingEquityAndInclusionScore || data.advancedForecastRating) && <Grid
        item
        sm={4}
        className={`${styles.paintContainer} ${styles.essentialContainerSpacing}`}
      >
        <EssentialRadialRating
          title={isAdvanced ? 'Advanced Forecast Rating' : 'Essential Equity & Inclusion Rating'}
          styles={styles}
          data={data[isAdvanced ? 'advancedForecastRating' : 'essentialRatingEquityAndInclusionScore']}
          renderSingleProp={renderSingleProp}
        />
      </Grid>}
    </Grid>
  }</>
  );
}

export default Essentials;