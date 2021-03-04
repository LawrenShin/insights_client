import React, {useEffect, useState} from 'react';
import useStyles from "./useStyles";
import {TextField} from "@material-ui/core";
import Button from "../button";


const Search = () => {
  const [search, setSearch] = useState<string>('');
  const [timer, setTimer] = useState<any>(null);
  const styles = useStyles();

  useEffect(() => {
    if (timer) {
      clearTimeout(timer);
      setTimer(null);
    }
    setTimer(setTimeout(() => console.log('request'), 500));
  }, [search]);

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
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <Button
          className={`${styles.button}`}
          disabled={!search}
          type={'button'}
        >
          Show all results
        </Button>
      </div>
    </div>
  )
}

export default Search;
