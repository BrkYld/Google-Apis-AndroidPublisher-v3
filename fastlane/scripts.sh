## for metadata update/upload
fastlane supply --skip_upload_apk true --skip_upload_aab true --changes_not_sent_for_review true --sync_image_upload true  --version_code "121236"

## for upload binary (aab)
## If we want to upload a binary using Fastlane, 
## we need to create a draft release. Therefore, we create a draft release in the 'internal' channel, which is the least risky channel, and upload the binary there.
fastlane supply --track "internal" --track_promote_release_status "draft" --release_status "draft" --skip_upload_apk true --skip_upload_aab false --skip_upload_metadata true --skip_upload_changelogs true --skip_upload_images true --skip_upload_screenshots true --aab "AAB_PATH"

## for upload binary (apk)
## If we want to upload a binary using Fastlane, 
## we need to create a draft release. Therefore, we create a draft release in the 'internal' channel, which is the least risky channel, and upload the binary there.
fastlane supply --track "internal" --track_promote_release_status "draft" --release_status "draft" --skip_upload_apk false --skip_upload_aab true --skip_upload_metadata true --skip_upload_changelogs true --skip_upload_images true --skip_upload_screenshots true --apk "APK_PATH" 

