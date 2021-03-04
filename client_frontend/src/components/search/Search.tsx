import React from 'react';
import useStyles from "./useStyles";
import {TextField} from "@material-ui/core";
import Button from "../button";


const Search = () => {
  const styles = useStyles();

  return (
    <div className={styles.container}>
      <div className={styles.tabs}>
        <div><span>Companies database</span></div>
      </div>
      <div className={styles.inputContainer}>
        <TextField
          type={'text'}
          size={'small'}
          name={'search'}
          label={'Search...'}
          variant="outlined"
          // error={}
          // helperText={}
          // value={values.username}
        />
        <Button
          className={`${styles.button}`}
          type={'button'}
        >
          Show all results
        </Button>
      </div>
    </div>
  )
}

export default Search;
