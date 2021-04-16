import React from 'react';
import useStyles from "./useStyles";
import {Typography} from "@material-ui/core";
import {Search} from "@material-ui/icons";

const SearchSettings  = () => {
  const styles = useStyles();

  return (
    <div className={styles.container}>
      <div className={styles.titlesContainer}>
        <Typography variant={'h4'}>Welcome to DI insights!</Typography>
        <span>Are you ready to explore?</span>
      </div>
      <div className={styles.searchExplanation}>
        <div className={styles.iconContainer}>
          <Search />
        </div>
        <div>
          <Typography variant={'h6'}>Search bar</Typography>
          <span>Just use the search bar if you want to find a specific company, industry or country. Enter a keyword and get the result in seconds.</span>
        </div>
      </div>
    </div>
  )
}

export default SearchSettings;
