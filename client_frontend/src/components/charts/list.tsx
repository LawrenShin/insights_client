import React from 'react';
import {CircularProgress, Grid} from "@material-ui/core";
import {keyTitle} from "../../helpers";
import useStyles, {paintRating, setBarWidth} from "./useStyles";

const renderBar = (key: string, data: {rating: string, score: number, strength: number}, styles: any) => <Grid
  item
  key={key+data.rating}
>
    <Grid container direction={'row'} alignContent={'center'}>
      <Grid item sm={4}>
       <span>{keyTitle(key.replace(/advanced|essential|score/gi, ''))}</span>
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
</Grid>;

const List = ({data}: any) => {
  const styles = useStyles();

  if (!data) return <CircularProgress />

  return (<Grid container direction={'column'} className={`${styles.gap5} ${styles.paddingH}`}>
    {Object.keys(data).map((key) => renderBar(key, data[key], styles))}
  </Grid>)
}

export default List;
