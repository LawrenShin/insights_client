import React from 'react';
import useStyles from "./useStyles";

const Research = () => {
  const styles = useStyles();

  return <div className={styles.researchContainer}>
      <span className={styles.noDataText}>No relevant Diversity, Equity & Inclusion news detected</span>
  </div>
}

export default Research;