import React from 'react';
import useStyles from "./useStyles";

const Research = () => {
  const styles = useStyles();

  return <div className={styles.researchContainer}>
      <span className={styles.noDataText}>No relevant D&I news detected</span>
  </div>
}

export default Research;