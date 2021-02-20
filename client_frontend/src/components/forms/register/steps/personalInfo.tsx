import React from 'react';
import {Grid, Typography} from "@material-ui/core";
import Input from "../../../formFields/input";
import {FormFieldType} from "../formModel";
import useStyles from "./useStyles";

interface Props {
  formField: FormFieldType;
}

const PersonalInfo = (props: Props) => {
  const {
    formField: {
      firstName,
      lastName,
      email,
      phoneNumber,
    }
  } = props;
  const styles = useStyles();

  return (
    <>
      <Typography className={styles.title} variant="h5" gutterBottom>
        Personal information
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={12} sm={6}>
          <Input
            name={firstName.name}
            label={firstName.label}
            variant="outlined"
            size={'small'}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
          <Input
            name={lastName.name}
            label={lastName.label}
            variant="outlined"
            size={'small'}
          />
        </Grid>
        <Grid item sm={12}>
          <Input
            className={styles.fullWidth}
            name={email.name}
            label={email.label}
            variant="outlined"
            size={'small'}
          />
        </Grid>
        <Grid item sm={12}>
          <Input
            className={styles.fullWidth}
            name={phoneNumber.name}
            label={phoneNumber.label}
            variant="outlined"
            size={'small'}
          />
        </Grid>
      </Grid>
    </>
  )
}

export default PersonalInfo;
