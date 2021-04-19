import React from 'react';
import useStyles, {paintLegend} from "./useStyles";
import {keyTitle} from "../../helpers";

type ObjLike = {
  [key: string]: {
    [key: string]: string | number,
  }
}
type ArrLike = { name: string, percentage: number }[];
interface Props {
  legend: ObjLike | ArrLike,
}

const renderItem = <S extends { [key: string]: string }>(value: string, styles: S) =>
  <div
    key={`${value}${Math.random()}`}
    className={`${styles.legendItem} ${styles['legendItemBubble' || 'legendItemPie']}`}
  >
    {styles.legendItemPie && <div style={{background: paintLegend(value)}}></div>}
    <span className={`${styles.paleFont}`}>
      {keyTitle(value)}
    </span>
  </div>;

const Legend = ({ legend }: Props) => {
  const styles = useStyles();
  const {legendItemBubble, legendItemPie, ...restStyles} = styles;

  return (<div className={styles.legendContainer}>
    {Array.isArray(legend) ?
      legend.map((item) =>
        renderItem(item.name, {...restStyles, legendItemBubble}))
    :
      Object.keys(legend).map((name) =>
        renderItem(name, {...restStyles, legendItemPie}))}
  </div>);
}

export default Legend;