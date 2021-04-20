import React from 'react';
import useStyles, {paintLegend} from "./useStyles";
import {keyTitle} from "../../helpers";


type LegendItem = { name: string, percentage: number };
type ObjLike = {
  [key: string]: LegendItem,
}
type ArrLike = LegendItem[];

interface Props {
  legend: ObjLike | ArrLike,
}

// NOTE: logic gettin' too complex, a good thing to refactor this if adds 4th type
const renderItem = <S extends { [key: string]: string }>(
  value: { name: string, percentage: number },
  styles: S,
) => {
  return (<div
    key={`${value.name}${Math.random()}`}
    className={`${styles.legendItem} ${styles['legendItemBubble' || 'legendItemPie'] || ''}`}
  >
    {styles.legendItemPie && <div
      style={{background: paintLegend(value.name.toLowerCase())}}
    > </div>}
    <span className={`${styles.paleFont}`}>
      {keyTitle(value.name)}
    </span>
    {value.percentage ? <span className={`${styles.paleFont}`}>
      {value.percentage}%
    </span> : null}
  </div>);
}


const Legend = ({ legend }: Props) => {
  const styles = useStyles();
  const {legendItemBubble, legendItemPie, ...restStyles} = styles;

  return (<div className={styles.legendContainer}>
    {Array.isArray(legend) ?
      legend.map((item) =>
        renderItem(item, {...restStyles, legendItemBubble}))
    :
      Object.keys(legend).map((name) =>
        renderItem(legend[name], {...restStyles, legendItemPie}))}
  </div>);
}

export default Legend;