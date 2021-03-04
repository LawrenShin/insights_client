import React from 'react';
import Search from "../../components/search/Search";
import SearchSettings from "../../components/searchSettings";
import Header from "../../components/Header";
import useStyles from "./useStyles";


const SearchCompanies = () => {
  const styles = useStyles();

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>
        <div className={styles.searchContainer}>
          <Search />
        </div>
        <div className={styles.searchSettingsContainer}>
          <SearchSettings />
        </div>
      </div>
    </div>
  )
}

export default SearchCompanies;
