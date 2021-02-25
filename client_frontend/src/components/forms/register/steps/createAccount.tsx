import React from 'react';
import {Grid, Typography} from "@material-ui/core";
import useStyles from "./useStyles";
import {FormFieldType} from "../formModel";
import MySelect from "../../../formFields/select";
import MyCheckbox from "../../../formFields/checkbox";

interface Props {
  formField: FormFieldType;
}

const CreateAccount = (props: Props) => {
  const {
    formField: {
      reasonOfInterest,
      terms,
    }
  } = props;
  const styles = useStyles();

  return (<>
    <Typography className={styles.title} variant="h5" gutterBottom>
      Company information
    </Typography>
    <Grid container spacing={3}>
      <Grid item sm={12}>
        <MySelect
          size={'small'}
          className={styles.fullWidth}
          name={reasonOfInterest.name}
          label={reasonOfInterest.label}
        />
      </Grid>
      <Grid item sm={12}>
         <MyCheckbox
           required={true}
           size={'small'}
           className={`${styles.fullWidth} ${styles.colorPurp}`}
           name={terms.name}
           label={terms.label || <span style={{fontSize: '14px'}}>I acknowledge that I agree to the <a href="#">Terms of Use</a> and have read <a href="#">Privacy Police.</a></span>}
         />
      </Grid>
    </Grid>
    </>)
}

export default CreateAccount;
