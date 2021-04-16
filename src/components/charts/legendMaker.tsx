import React from 'react';
import useStyles, {paintLegend} from "./useStyles";

interface Props {
  legend: {
    [key: string]: {
      [key: string]: string | number,
    }
  }
}

const renderItem = <S extends { [key: string]: string }>(value: string, styles: S) =>
  <div
    key={`${value}${Math.random()}`}
    className={styles.legendItem}
  >
    <div style={{ background: paintLegend(value) }}> </div>
    <span className={`${styles.paleFont}`}>
      {value}
    </span>
  </div>;

const Legend = ({ legend }: Props) => {
  const styles = useStyles();

  return (<div className={styles.legendContainer}>
    {Object.keys(legend).map((name) => renderItem(name, styles))}
  </div>);
}

export default Legend;