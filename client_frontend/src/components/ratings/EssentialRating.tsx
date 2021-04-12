import {Grid} from "@material-ui/core";
import React from "react";
import RadialChart from "../charts/radialChart";


const renderEssentialRatings = (data: any, styles: any) => {
  const chartProps = {
    width: 200,
    height: 150,
    ringWidth: 30,
    needleHeightRatio: .4,
    currentValueText: data?.quality || 'N/A',
    valueTextFontSize: '13',
    data: data,
  };

  return <div className={styles.centerChart}>
    <RadialChart {...chartProps} />
  </div>
}
// NOTE: header info is a container which provides styles for spans from renderHeaderInfo. Needs refactor
const EssentialRadialRating = ({title, styles, data, renderHeaderInfo}: any) => {
  return (
    // TODO: refactor container div into component to wrap any content
    <>
      <span className={`${styles.titleFont} ${styles.titleSubFontSize}`}>{title}</span>
      {renderEssentialRatings(data, styles)}
      <Grid justify={'space-around'} container direction={'row'} className={styles.companyHeader}>
        {renderHeaderInfo('Rating', data.rating)}
        {renderHeaderInfo('Strength score', data.strength)}
      </Grid>
    </>
  );
}

export default EssentialRadialRating;
