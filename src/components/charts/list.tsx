import React from 'react';
import {CircularProgress, Grid} from "@material-ui/core";
import {keyTitle} from "../../helpers";
import useStyles, {paintRating, setBarWidth} from "./useStyles";

const renderBar = (key: string, data: {rating: string, score: number, strength: number}, styles: any) => {
  const clearKey = key.replace(/advanced|essential|score/gi, '');
  const clearKeyLength = keyTitle(clearKey).split(' ').length;

  return (
    <Grid
      item
      key={key + data.rating}
      style={{ fontSize: '.8em' }}
    >
      <Grid container direction={'row'} alignContent={'center'}
        style={{ maxHeight: clearKeyLength <= 2 ? '30px' : '60px' }}
      >
        <Grid item sm={4}>
          <span>{clearKey !== 'DI' ? keyTitle(clearKey) : clearKey}</span>
        </Grid>
        <Grid item sm={7} className={`${styles.flex} ${styles.padding10}`}>
          <div
            className={styles.ratingListBar}
            style={{
              width: setBarWidth(data.score),
              background: paintRating(data.score),
            }}> </div>
        </Grid>
        <Grid item sm={1} className={`${styles.flex}`}>
          <span>{data.score}</span>
        </Grid>
      </Grid>
    </Grid>
  )
};

const List = ({data, classes}: any) => {
  const styles = useStyles();

  if (!data) return <CircularProgress />

  return (<Grid container direction={'column'} className={`${styles.gap5} ${styles.paddingH} ${classes}`}>
    {Object.keys(data).map((key) => renderBar(key, data[key], styles))}
  </Grid>)
}

export default List;
