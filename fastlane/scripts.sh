## for metadata update/upload
fastlane supply --skip_upload_apk true --skip_upload_aab true --changes_not_sent_for_review true --sync_image_upload true  --version_code "121236"

## for upload binary (aab)
fastlane supply --track_promote_release_status "draft" --release_status "draft" --skip_upload_apk true --skip_upload_aab false --skip_upload_metadata true --skip_upload_changelogs true --skip_upload_images true --skip_upload_screenshots true --aab "AAB_PATH"

## for upload binary (apk)
fastlane supply --track_promote_release_status "draft" --release_status "draft" --skip_upload_apk false --skip_upload_aab true --skip_upload_metadata true --skip_upload_changelogs true --skip_upload_images true --skip_upload_screenshots true --apk "APK_PATH" 

