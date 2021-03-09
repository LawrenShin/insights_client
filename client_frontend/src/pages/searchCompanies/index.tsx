import React from 'react';
import LookupSearch from "../../components/lookupSearch";
import CustomSearch from "../../components/customSearch";
import Header from "../../components/Header";
import useStyles from "./useStyles";


const SearchCompanies = () => {
  const styles = useStyles();

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>
        <div className={styles.searchContainer}>
          <LookupSearch />
        </div>
        <div className={styles.searchSettingsContainer}>
          <CustomSearch />
        </div>
      </div>
    </div>
  )
}

export default SearchCompanies;
