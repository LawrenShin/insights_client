import React from 'react';
import useStyles from "./useStyles";
import Welcome from "../forms/register/steps/welcome";
import {paleGrey, purp} from "../colorConstants";
import {SimplePaintedChecbox, StyledCheckbox} from "../checkbox";

const SearchSettings  = () => {
  const styles = useStyles();

  return (
    <div className={styles.container}>
      <StyledCheckbox />
    </div>
  )
}

export default SearchSettings;
